using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    [System.Serializable]
    public class TimeOfDay
    {
        public float TimeAsFloat = 12.0f;
        public Color SkyColor = Color.cyan;
        public Color SunColor = Color.white;
    }

    //public float CurrentTime = 12.0f;
    public float RealtimeScale = 20f;
    public List<TimeOfDay> TimesOfDay = new List<TimeOfDay>();

    public GameObject SkyPlane;
    public Light SkyLight;

    private TimeOfDay mCurrentTimeOfDay;
    private TimeOfDay mNextTimeOfDay;

    public TimeSpan CurrentTime = new TimeSpan(10,0,0);
    public GameObject Sky;

    private const float kDayLength = 24f; //24 hours, naturally
    private const float kBaseHundredConverter = 1.666666666666667f;

    [System.Serializable]
    public class TimeSpan
    {
        private int mHours = 0;
        private int mMinutes = 0;
        private float mSeconds = 0;

        public TimeSpan(int hours, int minutes, int seconds)
        {
            mHours = hours;
            mMinutes = minutes;
            mSeconds = seconds;
        }

        public float GetHoursAndMinutes(bool convertToBaseHundred)
        {
            return convertToBaseHundred ? mHours + ((mMinutes * 0.01f) * kBaseHundredConverter): mHours + (mMinutes * 0.01f);
        }

        public void AddSeconds(float seconds)
        {
            mSeconds += seconds;

            if (mSeconds > 59)
            {
                mSeconds -= 59;
                Minutes += 1;
            }
        }

        public float Seconds
        {
            get
            {
                return mSeconds;
            }
            set
            {
                mSeconds = value;

                if (mSeconds > 59)
                {
                    mSeconds -= 59;
                    Minutes += 1;
                }
            }
        }

        public int Minutes
        {
            get
            {
                return mMinutes;
            }
            set
            {
                mMinutes = value;

                if (mMinutes > 59)
                {
                    mMinutes -= 59;
                    Hours += 1;
                }
            }
        }

        public int Hours
        {
            get
            {
                return mHours;
            }
            set
            {
                mHours = value;

                if (mHours > 23)
                {
                    mHours -= 23;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Hours.ToString("00"), Minutes.ToString("00"), Seconds.ToString("00"));
        }

    }

    // Use this for initialization
    void Start ()
    {

    }
    
    // Update is called once per frame
    void Update ()
    {
        //CurrentTime += (Time.deltaTime * RealtimeScale);
        CurrentTime.AddSeconds(Time.deltaTime * RealtimeScale);

        if (CurrentTime.GetHoursAndMinutes(true) >= kDayLength)
        {
            CurrentTime = new TimeSpan(0,0,0);
            mCurrentTimeOfDay = TimesOfDay[0];
            mNextTimeOfDay = TimesOfDay[1];
        }

        for (int i = 0; i < TimesOfDay.Count; i++)
        {
            if(CurrentTime.GetHoursAndMinutes(true) > TimesOfDay[i].TimeAsFloat)
            {
                mCurrentTimeOfDay = TimesOfDay[i];
                mNextTimeOfDay = TimesOfDay[i + 1];
            }
        }


        Sky.transform.position = new Vector3(TycoonMainCamera.Instance.transform.position.x, TycoonMainCamera.Instance.transform.position.y, Sky.transform.position.z);

        //TODO: PRecalc this lerp so we aren't doing it twice
        SkyPlane.renderer.sharedMaterial.color = Color.Lerp(mCurrentTimeOfDay.SkyColor, mNextTimeOfDay.SkyColor,( (CurrentTime.GetHoursAndMinutes(true) - mCurrentTimeOfDay.TimeAsFloat) / (mNextTimeOfDay.TimeAsFloat - mCurrentTimeOfDay.TimeAsFloat) ) );
        
        SkyLight.light.color = Color.Lerp(mCurrentTimeOfDay.SunColor, mNextTimeOfDay.SunColor, ( (CurrentTime.GetHoursAndMinutes(true) - mCurrentTimeOfDay.TimeAsFloat) / (mNextTimeOfDay.TimeAsFloat - mCurrentTimeOfDay.TimeAsFloat) ));
        SkyLight.light.color = Color.Lerp(SkyLight.light.color, Color.black, Mathf.Clamp(TycoonPlayer.Instance.transform.position.y / TycoonMainCamera.Instance.MaxDepth, 0f, 1f));
    }
}
