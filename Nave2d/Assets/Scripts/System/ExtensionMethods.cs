using UnityEngine;
using System.Collections;
using System.Text;
using System.Globalization;
using System;
using System.Collections.Generic;

public static class ExtensionMethods {

	public static bool IsNullOrWhiteSpace(this String value) {
		if (value == null) return true;
		
		for(int i = 0; i < value.Length; i++) {
			if(!Char.IsWhiteSpace(value[i])) return false;
		}
		
		return true;
	}
}
