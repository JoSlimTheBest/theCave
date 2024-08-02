﻿
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator {
	public class LanguageManager : MonoBehaviour {
		#region Data
		[SerializeField] bool autoDetectLanguage=true;
		[SerializeField] Language defaultLanguage = Language.Russian;
		[SerializeField] Language _currentLang=Language.Russian;

        public static Language currentGlobalLang = Language.Russian;
		public int yes = 1;
		static LanguageManager _instance;
		static LanguageManager instance {
			get {
				if (_instance == null) {
					_instance = FindObjectOfType<LanguageManager> ();
					if (_instance == null) {
						GameObject obj = new GameObject ("LanguageManager");
						DontDestroyOnLoad (obj);
						_instance = obj.AddComponent<LanguageManager> ();
					}
				}
				return _instance;
			}
			set {
				_instance = value;
			}
		}
		static List<Label> labels {
			get {
				return LabelsData.labels;
			}
		}

        static bool initLang = false;
        #endregion

        #region Interface
        public void SetEnglishLang()
        {
            currentLang = Language.English;
        }

        public void SetRussianLang()
        {
            currentLang = Language.Russian;
		}

		public void SetSpanishLang()
		{
			currentLang = Language.Spanish;
		}
        public void SetFranceLang()
        {
            currentLang = Language.France;
        }

        public static Language currentLang {
			get {
				return instance._currentLang;
			}
			set {
                Debug.Log("LanguageManager: language " + value);
                currentGlobalLang = value;
                initLang = true;
                instance._currentLang = value;
                if (onLanguageChanged != null)
					onLanguageChanged ();
			}
		}

		public static Action onLanguageChanged;

		public static Label GetLabel (int labelID) {
			for (int i = 0; i < labels.Count; i++) {
				if (labels [i].id == labelID) {
					return labels [i];
				}
			}
			return LabelsData.defaultLabel;
		}

		public static string GetString(int labelID) {
			for (int i = 0; i < labels.Count; i++) {
				if (labels [i].id == labelID) {
					return labels [i].Get (currentLang);
				}
			}
			return LabelsData.defaultLabel.Get (currentLang);
		}

        public static string GetString(int labelID, Language lang)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                if (labels[i].id == labelID)
                {
                    return labels[i].Get(lang);
                }
            }
            return LabelsData.defaultLabel.Get(lang);
        }

        public static Language GetSystemLanguage() {
			switch (Application.systemLanguage) {
			case SystemLanguage.Ukrainian:
				return Language.Russian;
			case SystemLanguage.Belarusian:
				return Language.Russian;
			case SystemLanguage.Russian:
				return Language.Russian;
			case SystemLanguage.English:
				return Language.English;
			default:
				return instance.defaultLanguage;
			}
		}
		#endregion

		#region Methods
		void OnEnable() {
			if (_instance == null) {
				instance = this;
			} 
		}

		void Start() {


			if (PlayerPrefs.HasKey("lang"))
			{
				if(PlayerPrefs.GetInt("lang") == 1)
				{
					currentGlobalLang = Language.English;
                    currentLang = currentGlobalLang;
					yes = 2;
					return;
                }

				if(PlayerPrefs.GetInt("lang") == 2)
				{
                    currentGlobalLang = Language.Russian;
                    currentLang = currentGlobalLang;
                    yes = 3;
                    return;
                }


				if (PlayerPrefs.GetInt("lang") == 3)
				{
					currentGlobalLang = Language.Spanish;
					currentLang = currentGlobalLang;
					yes = 1;
                    return;
                }
                if (PlayerPrefs.GetInt("lang") == 4)
                {
                    currentGlobalLang = Language.France;
                    currentLang = currentGlobalLang;
                    yes = 1;
                    return;
                }
            }
			else
			{
                if (initLang)
                {
                    currentLang = currentGlobalLang;
                }
                else
                {
                    currentLang = autoDetectLanguage ? GetSystemLanguage() : defaultLanguage;
                    Debug.Log("LanguageManager: initialized. Current language: " + currentLang);
                }
            }
           
		}

		public void TakeLanguage()
        {


			if (initLang)
			{
				if(yes == 1)
                {
					currentLang = Language.English;
					PlayerPrefs.SetInt("lang",1);

                    yes = 2;
					return;
				}
                if(yes == 2)
                {
					
				    currentLang = Language.Russian;
					yes = 3;
                    PlayerPrefs.SetInt("lang",2);
                    return;
                }

				if(yes == 3)
				{
                    currentLang = Language.Spanish;
                    yes = 4;
                    PlayerPrefs.SetInt("lang", 3);
                    return;
                }

				if(yes == 4)
				{
                    currentLang = Language.France;
                    yes = 1;
                    PlayerPrefs.SetInt("lang", 4);
                    return;
                }
				
			}
			else
			{
				currentLang = autoDetectLanguage ? GetSystemLanguage() : defaultLanguage;
				Debug.Log("LanguageManager: initialized. Current language: " + currentLang);
			}
		}
		#endregion
	}
}