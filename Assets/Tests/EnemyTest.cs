using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{
    [UnityTest]
    public IEnumerator EnemyReactionOnCharacter()
    {
        // Тест проверяет реакцию врага на присутствие пользователя
        GameObject enemyGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Enemy"));
        GameObject charachterGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Character"));

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        Character character = charachterGameObject.GetComponent<Character>();
        
        Vector2 vector2 = new Vector2(enemy.transform.position.x+0.1f, enemy.transform.position.y);

        character.transform.position = vector2;

        yield return new WaitForSeconds(2.0f);

        // GameObject targetPlayer = enemyGameObject.GetTargetPlayer();
        // Не должно быть null
        
        Object.Destroy(enemy);
        Object.Destroy(character);
    }
    
    [UnityTest]
    public IEnumerator EnemyReactionOnInput()
    {
        // Тест проверяет реакцию врага на присутствие кнопки ввода
        GameObject enemyGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Enemy"));
        GameObject logicalInputGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Logical Input"));

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        LogicalInput logicalInput = logicalInputGameObject.GetComponent<LogicalInput>();

        Vector2 vector2 = new Vector2(enemy.transform.position.x+0.1f, enemy.transform.position.y);
        logicalInput.transform.position = vector2;

        yield return new WaitForSeconds(2.0f);

        // GameObject targetInput = enemyGameObject.GetTargetInput();
        // Не должно быть null
        
        Object.Destroy(enemy);
        Object.Destroy(logicalInput);
    }
    
    [UnityTest]
    public IEnumerator EnemyReactionOnCollider()
    {
        // Тест проверяет реакцию врага на препядствие перед ним
        /*GameObject enemyGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Enemy"));
        GameObject pistonGameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Piston"));

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        LogicalInput piston = pistonGameObject.GetComponent<LogicalInput>();

        Vector2 vector2 = new Vector2(enemy.transform.position.x+0.1f, enemy.transform.position.y);
        piston.transform.position = vector2;
        
        */
        yield return new WaitForSeconds(1.0f);
/*
        

        Object.Destroy(enemy);
        Object.Destroy(piston);
        */
    }
}
