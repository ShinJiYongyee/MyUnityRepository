using FoodyGo.Services.GoogleMaps;
using FoodyGo.Services.GPS;
using System;
using UnityEngine;

namespace FoodyGo.Mapping
{
    public class GoogleMapTile : MonoBehaviour
    {
        [Header("Map Settings")]
        [Tooltip("줌 레벨")]
        [Range(1, 20)]
        public int zoomLevel = 15;

        [Tooltip("맵 텍스쳐 사이즈")]
        [Range(64, 1024)]
        public int size = 640;

        [Tooltip("월드 맵 원점")]
        public MapLocation worldCenterLocation;

        [Header("Tile Settings")]
        [Tooltip("타일링을 위한 정수 오프셋")]
        public Vector2 tileOffset;

        [Tooltip("오프셋 적용한 맵의 중심 위치")]
        public MapLocation tileCenterLocation;

        [Header("Map Services")]
        public GoogleStaticMapService googleStaticMapService;

        [Header("GPS Services")]
        public GPSLocationService gpsLocationService;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            gpsLocationService.onMapRedraw += RefreshMapTile;
        }

        private void OnDisable()
        {
            gpsLocationService.onMapRedraw -= RefreshMapTile;
        }

        private void Start()
        {
            RefreshMapTile();
        }

        public void RefreshMapTile()
        {
            // offset에 따른 중심 위치 계산
            tileCenterLocation.latitude = GoogleMapUtils.AdjustLatByPixels(
                worldCenterLocation.latitude, (int)(tileOffset.y * size), zoomLevel);
            tileCenterLocation.longitude = GoogleMapUtils.AdjustLonByPixels(
                worldCenterLocation.longitude, (int)(tileOffset.x * size), zoomLevel);

            // 맵 텍스쳐 요청
            googleStaticMapService.LoadMap(tileCenterLocation.latitude,
                                            tileCenterLocation.longitude,
                                            zoomLevel,
                                            new Vector2(size, size),
                                            OnMapLoaded);

        }

        private void OnMapLoaded(Texture2D texture)
        {
            // 기존 텍스쳐 파괴
            if(_renderer.material.mainTexture != null)
            {
                Destroy(_renderer.material.mainTexture);
            }

            // 텍스쳐 갱신
            _renderer.material.mainTexture = texture;
        }
    }
}
