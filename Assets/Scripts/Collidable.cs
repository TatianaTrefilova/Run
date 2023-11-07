using UnityEngine;

public class Collidable : MonoBehaviour
{
	// Переменные для скрипта менеджера игры — 
	// скорость движения и величина изменения времени
	public GameManager manager;
	public float moveSpeed = 20f;
	public float timeAmount = 1.5f;

	void Update()
	{
		// При каждом вызове метода Update() объект перемещается 
		transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
	}

	// Cтолкновение объекта с чем-либо
	void OnTriggerEnter(Collider other)
	{
		// Cтолкновение с игроком? 
		if (other.tag == "Player")
		{
			// Сообщает менеджеру игры
			manager.AdjustTime(timeAmount);
			// Уничтожает объект
			Destroy(gameObject);
		}
	}
}
