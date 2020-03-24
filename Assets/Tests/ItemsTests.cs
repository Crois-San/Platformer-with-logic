using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class ItemsTests
{
    [UnityTest]
    public IEnumerator PistonActionTest()
    {
        // Находит ли поршень дверь под собой и началась ли анимация
        GameObject pistonGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Piston"));
        GameObject tileGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
        GameObject inputGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Logical Input"));
        LogicalPiston piston = pistonGameObject.GetComponent<LogicalPiston>();
        LogicalInput le1 = inputGameObject.GetComponent<LogicalInput>();
        
        tileGameObject.transform.position = new Vector2(piston.transform.position.x, piston.transform.position.y-2.5F);
        le1.transform.position = new Vector2(piston.transform.position.x-3.0F, piston.transform.position.y);
        le1.state = true;
        piston.SetLE1 = le1;

        yield return new WaitForSeconds(1.0f);
    
        // Проверка соединения
        Assert.IsTrue(piston.getConnected);
    
        // Проверка анимации
        Assert.IsFalse(piston.isNotPlaying);
    }


    [UnityTest]
    public IEnumerator LogicalElementsTest()
    {
        // Работоспособность логических элементов
        GameObject logicalInputGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Logical Input"));
        GameObject logicalNotGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Logical Not"));
        LogicalNot not = logicalNotGameObject.GetComponent<LogicalNot>();
        LogicalInput i = logicalInputGameObject.GetComponent<LogicalInput>();
        i.transform.position = new Vector2(not.transform.position.x-5.0F, not.transform.position.y);
        i.state = true;


        // ОТРИЦАНИЕ
        not.SetLE1 = i;
        yield return new WaitForSeconds(2.0f);
        Assert.IsFalse(not.state);

        // И
        GameObject logicalAndGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Logical And"));
        LogicalAnd and = logicalAndGameObject.GetComponent<LogicalAnd>();
        and.SetLE1 = i;
        and.SetLE2 = not;
        yield return new WaitForSeconds(2.0f);
        Assert.IsFalse(and.state);
        Object.Destroy(logicalAndGameObject);
        Object.Destroy(and);
    }
}




