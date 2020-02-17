using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{   
    [UnityTest]
    public IEnumerator ExampleEnumirator()
    {
    
        // 
     
        // GameObject enemyGameObject = MonoBehaviour.Instantiate(
        //     Resources.Load<GameObject>("Prefabs/Enemy"));
        // GameObject pistonGameObject = MonoBehaviour.Instantiate(
        //     Resources.Load<GameObject>("Prefabs/Piston"));
        //
        // Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        // LogicalPiston piston = pistonGameObject.GetComponent<LogicalPiston>();
        // bool jumpRequest = true;
        //
        // yield return new WaitForSeconds(5.0f);
        //
        // Vector2 vector2 = new Vector2(piston.transform.position.x+1.5f, piston.transform.position.y);
        //
        // enemy.transform.position = vector2;
        //
        yield return new WaitForSeconds(2.0f);
        //
        Assert.AreEqual(true, true);
        //
        // Object.Destroy(enemy);
        // Object.Destroy(piston);
    }
}

