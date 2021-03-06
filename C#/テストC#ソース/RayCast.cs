


	Ray ray = new Ray();
	RaycastHit hit = new RaycastHit();
	ray = Camera.main.ScreenPointToRay(Input.mousePosition);	//Rayを飛ばす位置を格納（マウスの位置）

	// Rayが何らかのオブジェクトにヒットした場合
	if(Physics.Raycast(ray.origin,ray.direction,out hit,Mathf.Infinity))
	{
		return hit.collider.gameObject.name;
	}

	// メインカメラの位置からメインカメラの方向に向かって投げる
	if(Physics.Raycast(メインカメラ.transform.position, メインカメラ.transform.forward, out hit, 5))
	{
		Destroy(hit.collider.gameObject);
	}

