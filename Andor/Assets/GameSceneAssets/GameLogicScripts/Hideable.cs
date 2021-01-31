using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public interface Hideable
	{
		void hide();
		bool isHidden();
		void reveal();
	}
}
