using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour {

	private float tumblingDuration = .3f; // Продолжительность поворота
	private bool isTumbling = false; // Определяет поворачивается ли куб в данный момент
	private Vector3 direction; // Направление движения

	void Update () {
        // В зависимотсти от нажатой кнопки присваиваем переменной direction соответствующее направление
        if (Input.GetKey (KeyCode.W)) {
			direction = Vector3.forward;
		}
		if (Input.GetKey (KeyCode.S)) {
			direction = Vector3.back;
		}
		if (Input.GetKey (KeyCode.A)) {
			direction = Vector3.left;
		}
		if (Input.GetKey (KeyCode.D)) {
			direction = Vector3.right;
		}

		if (direction != Vector3.zero && !isTumbling) {
			StartCoroutine (Tumble (direction)); // Запускаем сопрограмму
		}
	}

	IEnumerator Tumble (Vector3 dir) {
		isTumbling = true; // Начало поворота 
		
		Vector3 rotationAxis = Vector3.Cross (Vector3.up, dir); // Определение осьи вращения
		Vector3 pivotPosition = (transform.position + Vector3.down * 0.5f) + dir * 0.5f; // Определение точки вращения

		Quaternion startRotation = transform.rotation; 
		Quaternion endRotation = Quaternion.AngleAxis 
                                           (90f, rotationAxis) * 
                                           startRotation;

		Vector3 startPosition = transform.position;
		Vector3 endPosition = transform.position + dir;

		float rotSpeed = 90f / tumblingDuration;
		float t = 0.0f;

        // Поворот куба вокруг точки pivotPosition
        while (t < tumblingDuration) {
            t += Time.deltaTime;
            if (t < tumblingDuration) {
                transform.RotateAround (pivotPosition,
                                        rotationAxis,
                                        rotSpeed * Time.deltaTime);
                yield return null;
            } else {
                transform.rotation = endRotation;
                transform.position = endPosition;
            }
		}

        direction = Vector3.zero; // Сброс направления на 0
        isTumbling = false; // Конец поворота
	}
}
