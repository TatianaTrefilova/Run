using UnityEngine;

public class GameManager : MonoBehaviour
{
	public TextureScroller ground;
	public float gameTime = 10;

	float totalTimeElapsed = 0;
	bool isGameOver = false;

	// Следим за временем
	void Update()
	{
		if (isGameOver)
			return;
	
	// Добавляем время с момента последнего кадра (Time.deltaTime) к переменной totalTimeElapsed
		totalTimeElapsed += Time.deltaTime;
		gameTime -= Time.deltaTime;

	// Если игра завершилась, устанавливаем флаг isGameOver
		if (gameTime <= 0)
			isGameOver = true;
	}

	// Метод вызывается, когда игрок сталкивается с бонусом или препятствием
	public void AdjustTime(float amount)
	{
		// Изменяем количество оставшегося времени 
		gameTime += amount;
		// Если значение изменения отрицательно (препятствие), 
		// данный метод вызывает метод замедления игры
		if (amount < 0)
			SlowWorldDown();
	}

	// Метод вызывается, когда игрок попадает в препятствие
	void SlowWorldDown()
	{
		// Отмена «ускорения» мира
		// Мир замедляется на 1 секунду
		CancelInvoke(); 
		Time.timeScale = 0.5f;
		// Вызов метода ускорения игры через 1 секунд
		Invoke("SpeedWorldUp", 1);
	}

	// Метод ускоряет игру, возвращая ее к нормальному темпу
	void SpeedWorldUp()
	{
		Time.timeScale = 1f;
	}
	// Обратите внимание, что здесь используется старая система интерфейсов Unity
	// Метод OnGUI выводит на экран оставшееся и прошедшее время игры
	void OnGUI()
	{
		if (!isGameOver)
		{
			Rect boxRect = new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50);
			GUI.Box(boxRect, "Time Remaining");

			Rect labelRect = new Rect(Screen.width / 2 - 10, Screen.height - 80, 20, 40);
			GUI.Label(labelRect, ((int)gameTime).ToString());
		}
		else
		{
			Rect boxRect = new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 120, 50);
			GUI.Box(boxRect, "Game Over");

			Rect labelRect = new Rect(Screen.width / 2 - 55, Screen.height / 2 - 80, 90, 40);
			GUI.Label(labelRect, "Total Time: " +(int)totalTimeElapsed);

			Time.timeScale = 0;
		}
	}
}
