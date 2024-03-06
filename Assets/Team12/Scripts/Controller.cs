using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team12
{
    public class Controller : MicrogameEvents
    {
        public static GameObject TaggedPlayer { get; private set; }
        public static GameObject player1;
        public static GameObject player2;

        public static bool setSinceLastTouch = false;

        private void Start()
        {
            player1 = GameObject.Find("Player 1");
            player2 = GameObject.Find("Player 2");
            SetTaggedPlayer(player2);
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
            } else if (TaggedPlayer == player2)
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
            }
            
        }
    }

}