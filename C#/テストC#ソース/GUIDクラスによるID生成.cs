
        // GUID生成の生成、表示、格納
        Guid guidValue = Guid.NewGuid();
        guidField.text = guidValue.ToString();
        guidField.interactable = true;
        gameManager.userGuid = guidValue.ToString();


