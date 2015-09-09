(
	function()
	{
		// -------------------------------------------------------------------- data
		var $data = {
			// PHP ��URL
			url		: "http://vote1.fc2.com/poll.php",
			
			// error -> 1, �ʏ�͋�
			error	: "",
			
			// user id
			uid		: "12029851",
			
			// ���[�^�C�g��
			title	: "",
			
			// ���[�U�̃j�b�N�l�[��
			nickname: "yohshiy",
			
			// ���[�U�w��̐F��
			clr		: "blue", // �s�v
			
			// ���[�U�w��̔w�i�摜
			bgimg	: "",
			
			// ����ԍ� : ���█�̃��j�[�N�ȃC���f�b�N�X�ԍ�
			qnum 	: "3",
			
			// ����
			quest 	: "�g���Ă��� JavaScript �n�V(�ϊ�)����� ?",
			
			// ���[�I������	: UTC ( 0�͖��ݒ� )
			period	: "0"-0,
			
			// ���ʉ{������	: 0-> ���ł��A1->�����[���͕s��
			vrest	: "0"-0,
			
			// �R�����g�擾 : 0-> �i�V�A1-> �C��, 2->�K�{
			cget	: "1"-0,
			
			// �R�����g�ő啶����
			cleng	: "25"-0,
			
			// �R�����g�{������	: 0-> �N�ł���, 1-> �Ǘ��҂̂�
			crest	: "0"-0,
			
			// �A�����e�֎~ : 0->No, 1->Yes
			pkick	: "1"-0,
			
			// �A�����e�֎~���� ( �b.  0�͖��ݒ� )
			kval	: "43200"-0,
			
			// ���[�͈�l��� :true->Yes, false->No
			onetime : !!("43200"-0 && !"1"),
			
			// ���[�� : 0-> �i�s��, 1->�폜�ς�, 2-> �I��
			ing		: "0"-0,
			
			// �I�����̒ǉ���������Ă���ꍇ
			// �ǉ��ł���I�����̍ő�l������B
			// env.choice[N].extra ���J�E���g���A�ǉ��\���ۂ�����ł���B
			// ������Ă��Ȃ��ꍇ�̒l�� 0.
			usraddlen	: "0"-0,
			
			// �I�����̒ǉ��Ɋւ���l
			/* radio ���i
			 * <input type=radio name=env.extra_radioname value=env.extra_radioval
			 * �ǉ����̓t�H�[��
			 * <input type=text name=env.extra_textname
			 */
			extra_radioname : "poll",
			extra_radioval 	: "0",
			extra_textname 	: "usrsel",
			
			// �R�����g���̓t�H�[�� name �����̒l
			comment_textname: "com",
			
			// �e�I�����Ƃ���Ɋւ���l
			/* <img src=env.choice[N].img >
			 * <input type=radio name=env.choice[N].name value=env.choice[N].value >
			 *
			 * env.choice[N].text : ���╶
			 * env.choice[N].extra : 0-> �Ǘ��҂̗p�ӂ����I�����A 1->�ǉ����ꂽ�I����
			 */
			choice : [
								{
					name : "poll",
					value: "1",
					text : "Dart",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "2",
					text : "TypeScript",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "3",
					text : "CoffeeScript",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "4",
					text : "JSX",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "5",
					text : "Haxe",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "6",
					text : "���̑�",
					img  : "",
					extra: "0"-0
				},
								null
			],
			// hidden �p�p�����[�^ 
			// <input type=hidden name=env.hides[N].name value=env.hides[N].value
			hides 	: [
								{
					name : "uid",
					value: "12029851"
				},
								{
					name : "no",
					value: "3"
				},
								{
					name : "mode",
					value: "on"
				},
								{
					name : "charset",
					value: document.charset || document.characterSet
				}
			]
		};
		var $style_title_fg = '#ffffff';
		var $style_title_bg = '#669999';
		var $style_main_fg = '#000000';
		var $style_main_bg = '#ffffff';
		var $style_transparent = '';
		var $style_border = '#000000';
		var $width = 180;
		// ------------------------------------------------------------------- /data
		// -------------------------------------------------------------------- item
		// ID �v���t�B�N�X
		var $id_base = 'fc2_vote_' + $data['uid'] + '_' + $data['qnum'];
		// �ǉ��ϑI������
		var $count_extra = 0;
		// �I���� (�K�肨��ђǉ���)
		var $items = [];
		for (var $i=0; $i<$data['choice'].length; $i++)
		{
			var $obj = $data['choice'][$i];
			if (! $obj)
			{
				continue;
			}
			var $id = $id_base + '_' + $obj['value'];
			var $img = $obj['img'] ? '<img src="' + $obj['img']
				+ '" style="vertical-align:middle;margin:0px 3px;"/>' : '';
			$items.push(
				'<input type="radio" name="' + $obj['name']
					+ '" value="' + $obj['value'] + '" id="' + $id + '" />' + $img
					+ '<label for="' + $id + '" style="cursor:pointer;">'
					+ $obj['text'] + '</label>'
			);
			$count_extra += $obj['extra'];
		}
		// �I���� (�ǉ��t�H�[��)
		var $extra_id = [
			$id_base + '_0',
			$id_base + '_0_' + $data['comment_textname']
		];
		if ($count_extra < $data['usraddlen'])
		{
			$items.push(
				'<input type="radio" name="' + $data['extra_radioname']
					+ '" value="' + $data['extra_radioval']
					+ '" id="' + $extra_id[0] + '" />'
					+ '<input type="text" name="' + $data['extra_textname']
					+ '" id="' + $extra_id[1] + '" style="width:120px;height:20px;" />'
			);
		}
		// �I�����e�[�u����
		var $style = [
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'color:' + $style_main_fg,
			'text-align:left',
			'overflow:hidden'
		];
		$style = $implode(';', $style);
		var $txt = '';
		for (var $i=0; $i<$items.length; $i++)
		{
			$txt += '<tr><td style="' + $style + '">' + $items[$i] + '</td></tr>';
		}
		$items = $txt;
		var $style = [
			'border:0px',
			'border-spacing:0px',
			'border-collapse:collapse',
			'margin:auto',
			'width:' + ($width-6) + 'px',
			'table-layout:fixed'
		];
		$style = $implode(';', $style);
		$items = '<table style="' + $style + '">' + $items + '</table>';
		// �s�������p�����[�^
		for (var $i=0; $i<$data['hides'].length; $i++)
		{
			var $obj = $data['hides'][$i];
			if (! $obj)
			{
				continue;
			}
			$items += '<input type="hidden" name="' + $obj['name']
				+ '" value="' + $obj['value'] + '" />';
		}
		// ------------------------------------------------------------------- /item
		// -------------------------------------------------------------------- html
		var $html = '';
		// �w�b�_
		var $style = [
			'color:' + $style_title_fg,
			'text-decoration:none'
		];
		$txt = '<a href="http://vote.fc2.com/" target="_blank" style="'
			+ $implode(';', $style) +'">FC2�������[�����^��</a>'
		var $style = [
			'color:' + $style_title_fg,
			'background-color:' + $style_title_bg,
			'padding:3px 0px 3px 4px'
		];
		$html += $div(
			$txt,
			$style
		);
		// ����
		var $style = [
			'background-color:#eeeeee',
			'padding:4px 2px 4px 4px'
		];
		$html += $div($data['quest'], $style);
		// �A�C�e��
		var $style = [
			'border-top:1px solid ' + $style_border,
			'padding:2px 2px 6px 2px',
			'text-align:center'
		];
		if ($data['bgimg'])
		{
			$style.push('background-image:url(' + $data['bgimg'] +')');
		}
		if (! $style_transparent)
		{
			$style.push('background-color:' + $style_main_bg);
		}
		$html += $div($items, $style);
		// �R�����g
		var $comment = '';
		if ($data['cget'])
		{
			var $style = [
				'padding:1px 0px 0px 6px',
				'text-align:left'
			];
			$comment += $div(
				$data['cleng'] + '�����ȓ��̃R�����g'
					+ ($data['cget']>1 ? ' �i�K�{�j' : ''),
				$style
			);
			var $style = [
				'padding:0px 0px 2px 8px',
				'text-align:left'
			];
			$comment += $div(
				'<input type="text" name="' + $data['comment_textname']
					+ '" id="' + $id_base + $data['comment_textname']
					+ '" style="width:' + ($width-16) + 'px;height:20px;" />',
				$style
			);
		}
		// ���[���ʃ����N
		var $result_link = '';
		if (! $data['vrest'])
		{
			var $style = [
				'padding:8px 0px 0px 8px',
				'float:left'
			];
			$result_link = $div(
				'<a href="' + $data['url']
					+ '?mode=result&amp;uid=' + $data['uid'] + '&amp;no=' + $data['qnum']
					+ '" style="color:#777777;" target="_blank">���[����</a>',
				$style
			);
		}
		// ���M�{�^��
		var $style = [
			'border-top:1px solid ' + $style_border,
			'background-color:#eeeeee',
			'padding:4px 4px 4px 0px',
			'text-align:right'
		];
		$html += $div(
			$comment + $result_link + '<input type="button" value="���[" id="'
				+ $id_base + '_button" style="height:23px;" />',
			$style
		);
		// �O�g
		var $style = [
			'color:#000000',
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'border:1px solid ' + $style_border,
			'text-align:left',
			'width:' + $width + 'px'
		];
		$html = $div($html, $style);
		
		// PR�g
		var $style = [
			'background-color:#eeeeee',
			'color:#000000',
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'border-bottom:1px solid ' + $style_border,
			'border-left:1px solid ' + $style_border,
			'border-right:1px solid ' + $style_border,
			'text-align:left',
			'width:' + $width + 'px'
		];
		//$txt = '<script type="text/javascript" src="http://ad.pitta.ne.jp/ads/a38e375901ad047bed132971cbe2fc7fd6a83799"></script>'
		//$html += $div(
		//	$txt,
		//	$style
		//);
		
		// <form>
		$html = '<form method="post" name="'+ $id_base
			+ '" action="' + $data['url']
			+ '" target="_blank" style="margin:0px;">' + $html + '</form><img src="http://media.fc2.com/counter_img.php?id=715" width="1" height="1">';
		document.write($html);
		// ------------------------------------------------------------------- /html
		// ------------------------------------------------------------------- event
		// disable
		var $button = $($id_base + '_button');
		$button.disabled = true;
		// radio
		var $radios = document[$id_base].poll;
		for (var $i=0; $i<$radios.length; $i++)
		{
			$radios[$i].onclick = function()
			{
				$button.disabled = false;
			};
		}
		// extra
		var $obj = $($extra_id[1]);
		if ($obj)
		{
			$obj.onclick = function()
			{
				$($extra_id[0]).checked = true;
				$button.disabled = false;
			};
		}
		// submit
		$button.onclick = function()
		{
			var $checked = false;
			for (var $i=0; $i<$radios.length; $i++)
			{
				if ($radios[$i].checked)
				{
					$checked = $radios[$i].id;
					break;
				}
			}
			if ($checked==$extra_id[0] && ! $($extra_id[1]).value.length)
			{
				alert('�ǉ�����I�����̖��O���J���b�|�I');
				return;
			}
			if ($data['cget']>1)
			{
				if (! $($id_base + $data['comment_textname']).value.length)
				{
					alert('�R�����g������');
					return;
				}
			}
			$button.disabled = true;
			document[$id_base].submit();
		};
		// ------------------------------------------------------------------ /event
		// ------------------------------------------------------------------ common
		// implode()
		function $implode($sep, $src)
		{
			if (! $src || ! $src.length)
			{
				return '';
			}
			var $dst = $src.shift();
			while ($src.length)
			{
				var $txt = $src.shift();
				if ($txt)
				{
					$dst += $sep + $txt;
				}
			}
			return $dst;
		}
		// div()
		function $div($val, $style)
		{
			$style = $implode(';', $style);
			return '<div style="' + $style + '">' + $val + '</div>';
		}
		// $()
		function $($id)
		{
			return document.getElementById($id);
		}
		// ----------------------------------------------------------------- /common
	}
)();