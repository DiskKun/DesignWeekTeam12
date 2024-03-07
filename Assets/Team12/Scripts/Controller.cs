using UnityEngine;
using UnityEngine.Audio;

namespace team12
{
    public class Controller : MicrogameEvents
    {
        public static GameObject TaggedPlayer { get; private set; }
        public static GameObject player1;
        public static GameObject player2;

        public static bool setSinceLastTouch = false;

        public AudioSource audioSource;

        public AudioClip baseballBatSwing;
        public AudioClip disappearance;

        public AudioMixerSnapshot fadeOut;
        public AudioMixerSnapshot fadeIn;

        private void Start()
        {
            player1 = GameObject.Find("Player 1");
            player2 = GameObject.Find("Player 2");
            int r = Random.Range(1, 3);
            if (r == 1)
            {
                SetTaggedPlayer(player1);

            }
            else
            {
                SetTaggedPlayer(player2);
            }
            fadeOut.TransitionTo(0.1f);
            fadeIn.TransitionTo(4);
        }

        public static void SetTaggedPlayer(GameObject player)
        {
            if (setSinceLastTouch == false)
            {
                TaggedPlayer = player;
            }
        }

        public static void SwapTaggedPlayer()
        {
            if (TaggedPlayer == player1)
            {
                TaggedPlayer = player2;
            }
            else if (TaggedPlayer == player2)
            {
                TaggedPlayer = player1;
            }
        }

        private void Update()
        {
            if (setSinceLastTouch == true)
            {
                SwapTaggedPlayer();
                setSinceLastTouch = false;

                audioSource.PlayOneShot(baseballBatSwing);
            }
        }

        // This website was used for making the audio fade out: https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/#third_method
        protected override void OnTimesUp()
        {
            // Code to execute when time runs out in the game

            audioSource.PlayOneShot(disappearance);

            fadeOut.TransitionTo(4);
        }
    }
}