using UnityEngine;

public class Player : MonoBehaviour
{
	// Атрибут Header, с помощью которого на панели Inspector выводится строка заголовка 
	[Header("References")]
	// Переменные содержат ссылки на менеджера игры и два материала
	public GameManager manager;
	public Material normalMat;
	public Material phasedMat;

	// Переменные обрабатывают настройки игры — границы уровня
	// и скорость бокового движения игрока
	[Header("Gameplay")]
	public float bounds = 3f;
	public float strafeSpeed = 4f;
	public float phaseCooldown = 2f;

	Renderer mesh;
	Collider collision;
	bool canPhase = true;

	void Start()
	{
		mesh = GetComponentInChildren<SkinnedMeshRenderer>();
		collision = GetComponent<Collider>();
	}

	void Update()
	{
		// Перемещение игрока за счет управления
		float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * strafeSpeed;
		Vector3 position = transform.position;
		position.x += xMove;

		// Метод Mathf.Clamp() не дает игроку покидать коридор
		position.x = Mathf.Clamp(position.x, -bounds, bounds);
		transform.position = position;

		// Нажата ли клавиша Пробел («Jump») 
		if (Input.GetButtonDown("Jump") && canPhase)
		{
			// Игрок исчезает (то есть его коллайдер отключается), 
			// и игрок какое-то время становится прозрачным для препятствий 
			// и теряет способность подбирать бонусы
			canPhase = false;
			mesh.material = phasedMat;
			collision.enabled = false;

			// Вызов метода, возвращающего видимость игрока
			Invoke("PhaseIn", phaseCooldown);
		}
	}

	// Метод возвращает видимость игрока и способность подбирать бонусы
	void PhaseIn()
	{
		canPhase = true;
		mesh.material = normalMat;
		collision.enabled = true;
	}
}
