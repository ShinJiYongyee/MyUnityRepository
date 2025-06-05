using FoodyGo.Services.GoogleMaps;
using FoodyGo.Services.GPS;
using System;
using System.Collections;
using UnityEngine;

namespace FoodyGo.Mapping
{
    /// <summary>
    /// Maptile 생성, 갱신, 제거 등을 관리
    /// GPS 데이터가 범위를 벗어날 때 타일맵 확장, 지나온 방향 타일맵 삭제
    /// </summary>
    public class GoogleMapTileManager : MonoBehaviour
    {
        public bool isInitialized { get; private set; }

        [Header("Configuration")]
        [SerializeField] GoogleStaticMapService _googleStaticMapService;
        [SerializeField] GPSLocationService _gpsLocationService;
        [SerializeField] GoogleMapTile _mapTilePrefab;
        [SerializeField] Transform _mapTilesParent;

        [Header("Debug")]
        Vector2Int _currentCenterTileCoord;

        [Header("Managed mapTiles")]
        GoogleMapTile[,] _mapTiles = new GoogleMapTile[3, 3];
        readonly int[] TILE_OFFSETS = { -1, 0, 1 };

        IEnumerator Start() // Start() 함수는 그 자체로 코루틴 작성 가능, Awake()와 OnEnable()은 불가
        {
            yield return new WaitUntil(() => _gpsLocationService.isReady);
            InitializeTiles();
            isInitialized = true;
        }

        /// <summary>
        /// 현재 GPS 기반으로 중심 타일 인덱스 계산
        /// 3X3 배열로 MapTile 들 생성
        /// </summary>
        void InitializeTiles()
        {
            _currentCenterTileCoord = CalcTileCoordinate(_gpsLocationService.mapCenter);
            CreateTiles(_currentCenterTileCoord);
        }

        void CreateTiles(Vector2Int center)
        {
            // 중심 인덱스 기준으로 모든 방향 타일들 인덱스 계산
            for (int i = 0; i < TILE_OFFSETS.Length; i++)
            {
                for (int j = 0; j < TILE_OFFSETS.Length; j++)
                {
                    Vector2Int coord = new Vector2Int(center.x + TILE_OFFSETS[i],
                                                      center.y + TILE_OFFSETS[j]);

                    GoogleMapTile tile = Instantiate(_mapTilePrefab, _mapTilesParent);
                    tile.tileOffset = new Vector2Int(i - 1, j - 1);
                    tile.googleStaticMapService = _googleStaticMapService;
                    tile.zoomLevel = _gpsLocationService.mapTileZoomLevel;
                    tile.gpsLocationService = _gpsLocationService;
                    tile.name = $"MapTile_{coord.x}_{coord.y}";
                    tile.transform.position = CalcWorldPosition(coord);
                    tile.RefreshMapTile();
                    _mapTiles[i, j] = tile;
                }
            }
        }

        public Vector3 GetCenterTileWorldPosition()
        {
            return CalcWorldPosition(_currentCenterTileCoord);
        }

        /// <summary>
        /// 타일 인덱스로 게임월드 포지션 산출
        /// </summary>
        /// <param name="coord"> 타일 인덱스 </param>
        /// <returns> 월드 위치 </returns>
        Vector3 CalcWorldPosition(Vector2Int coord)
        {
            float spacing = 10f; // 매직 넘버
            return new Vector3(-coord.x * spacing, 0f, coord.y * spacing);

        }

        /// <summary>
        /// 특정 위도, 경도에 해당하는 MapTile의 인덱스를 계산
        /// </summary>
        /// <param name="center"> MapTile을 그릴 위도, 경도 중심</param>
        /// <returns> MapTile 인덱스 </returns>
        Vector2Int CalcTileCoordinate(MapLocation center)
        {
            // 메르카토르 픽셀 좌표 (zoom = 21)
            int pixelX21 = GoogleMapUtils.LonToX(center.longitude);
            int pixelY21 = GoogleMapUtils.LatToY(center.latitude);

            // GoogleMap zoomlevel 1 당 2배씩 작아지므로 
            // ZoomLevel 차이만큼 오른쪽으로 Bit-Shift 하여 원하는 픽셀값을 구할 수 있다.
            int shift = 21 - _gpsLocationService.mapTileZoomLevel;
            int pixelX = pixelX21 >> shift;
            int pixelY = pixelY21 >> shift;

            // MapTile 당 픽셀수로 나누어 인덱스 산출
            return new Vector2Int(Mathf.FloorToInt(pixelX / (float)_gpsLocationService.mapTileSizePixels),
                                  Mathf.FloorToInt(pixelY / (float)_gpsLocationService.mapTileSizePixels));

        }
    }
}
