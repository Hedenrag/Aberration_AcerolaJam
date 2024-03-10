using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hedenrag
{
    namespace ExVar
    {
        [Serializable]
        /// <summary>
        /// <para> Variable with a certain lifetime </para>
        /// <br>Most usefull setting a <c>bool</c> to <c>true</c> and after a time it sets back to <c>false</c></br>
        /// </summary>
        public struct TemporalVariable<T>
        {
            [SerializeField] private T value;
            [SerializeField] private double time;
            bool onTime;
            Timer timer;

            /// <summary>
            /// Original set time
            /// </summary>
            public double VariableDuration => time;

            /// <summary>
            /// <para>Returns the value if the time is not over, else it returns default value</para>
            /// <br>(default value is <c>null</c> for classes, <c>new()</c> for sructs and <c>0</c> for integrated variables)</br>
            /// </summary>
            public T Value
            {
                get
                {
                    if (onTime)
                    {
                        return value;
                    }
                    return default(T);
                }
            }

            /// <summary>
            /// Returns the original value regardless of the time
            /// </summary>
            public T OriginalValue => value;

            /// <summary>
            /// Creates a variable with a limited use time
            /// </summary>
            /// <param name="value">value that will be held</param>
            /// <param name="time">time in seconds until the cancel</param>
            public TemporalVariable(T value, double time)
            {
                this.value = value;
                this.time = time;
                this.onTime = true;

                timer = new Timer();
                timer.Interval = time;
                timer.AutoReset = false;
                timer.Elapsed += OnFinishTimer;
                timer.Start();
            }

            void OnFinishTimer(System.Object source, ElapsedEventArgs e)
            {
                onTime = false;
            }

            /// <summary>
            /// starts the timer again from the previous set time
            /// </summary>
            public void ResetTimer(bool resetCompletion = true)
            {
                SetTime(time, resetCompletion);
            }
            /// <summary>
            /// Restart the timer with a new time
            /// </summary>
            /// <param name="time">new time to wait in seconds</param>
            /// <param name="resetCompletion">if the time is over ¿should it make the variable alive again?</param>
            public void SetTime(double time, bool resetCompletion = true)
            {
                if (resetCompletion)
                {
                    onTime = true;
                }
                timer = new Timer();
                timer.Interval = time;
                timer.AutoReset = false;
                timer.Elapsed += OnFinishTimer;
                timer.Start();
            }

            #region operators

            public static bool operator true(TemporalVariable<T> tmpV) => tmpV.onTime;
            public static bool operator false(TemporalVariable<T> tmpV) => !tmpV.onTime;
            public static bool operator !(TemporalVariable<T> tmpV) => !(tmpV.onTime);


            //TODO add more operators for ease of use
#if NET_4_6

#endif

            #endregion
        }
    }
}