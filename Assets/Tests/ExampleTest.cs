using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExampleTest
{
    [UnityTest]
    public IEnumerator ExampleEnumirator()
    {
        // Тест проверяет работу кнопок при взаимодействии с пользователем
        GameObject characterGameObject = MonoBehaviour.Instantiate(
        Resources.Load<GameObject>("Prefabs/Character"));
        GameObject logicalInputGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Logical Input"));

        Character character = characterGameObject.GetComponent<Character>();
        LogicalInput logicalInput = logicalInputGameObject.GetComponent<LogicalInput>();
        bool expectedState = !logicalInput.GetState();

        logicalInput.transform.position = character.transform.position;

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual(expectedState, logicalInput.GetState());

        Object.Destroy(character);
        Object.Destroy(logicalInput);
    }
}

