using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{
    [UnityTest]
    public IEnumerator ExampleEnumirator()
    {
        // Тест проверяет работу кнопок при взаимодействии с пользователем
        GameObject enemyGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Enemy"));

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();


        yield return new WaitForSeconds(2.0f);
        
        Object.Destroy(enemy);
    }
}
