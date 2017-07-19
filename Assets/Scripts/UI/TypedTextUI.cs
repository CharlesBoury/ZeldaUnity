
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TypedTextUI : MonoBehaviour
{
	[SerializeField]
	private float waitingTime = 0.1f;
	[SerializeField]
	private float waitingPunctuationTime = 0.1f;

	[Header("Events")]
	[SerializeField]
	private UnityEvent printStart = new UnityEvent();
	[SerializeField]
	private UnityEvent printCompleted = new UnityEvent();

	private readonly List<char> punctutationCharacters = new List<char>
	{
		'.',
		',',
		'!',
		'?'
	};
	private readonly List<char> noDelayCharacters = new List<char>
	{
		' ',
		'\n',
		'\''
	};

	// internal
	private string parole;
	private float compteur = 0;
	private bool writing = false;



	void Awake()
	{
		parole = GetComponent<Text>().text;
	}
	void OnEnable()
	{
		StartWriting();
	}
	void StartWriting()
	{
		if (printStart != null) printStart.Invoke();
		compteur = 0;
		writing = true;
	}
	void Update()
	{
		if (writing)
		{
			compteur += Time.deltaTime;
			GetComponent<Text>().text = GetTextFromTime(parole, compteur);
		}
	}
	string GetTextFromTime(string s, float t)
	{
		string result = "";
		float teteDeLecture = 0;

		foreach (var ch in s) {
			if (teteDeLecture <= t) result += ch;
			teteDeLecture += GetCharTime(ch);
		}
		if (result.Length == s.Length) // fini
		{
			writing = false;
			if (printCompleted != null) printCompleted.Invoke();
			return result;
		}
		return result + "<color=#00000000>" + s.Substring(result.Length) + "</color>";
	}

	float GetCharTime(char c)
	{
		// 0 doesn't get time to get printed, it's instantaneous
		// 1 is the normal typingSpeed (class parameter)
		// 2 is twice as long as the defaultTypingSpeed, etc.
		if (punctutationCharacters.Contains(c)) return waitingPunctuationTime;
		else if (noDelayCharacters.Contains(c)) return 0f;
		else return waitingTime;
	}

}