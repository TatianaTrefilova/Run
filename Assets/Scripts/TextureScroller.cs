using UnityEngine;

public class TextureScroller : MonoBehaviour
{
	public float speed = .5f;
	
	Renderer mesh;
	float offset;

	void Start()
	{
		mesh = GetComponent<Renderer>();
	}

	void Update()
	{
		//Увеличение смещения в зависимости от времени
		offset += Time.deltaTime * speed;
		//Смещение в диапазоне от 0 до 1 
		if (offset > 1)
			offset -= 1;
		//Применение смещения к материалу
		mesh.material.mainTextureOffset = new Vector2(0, offset);
	}
}
