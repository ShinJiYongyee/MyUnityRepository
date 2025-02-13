using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DropTable", menuName ="DropTable/drop table", order =0)]
public class DropTable : ScriptableObject
{
    public List<GameObject> drop_table;
}
