using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace team12
{
    public class PlayerCoolDown : MicrogameEvents
    {
        public GameObject player;
        RectTransform rt;
        Slider s;
        public Player1 playerScript;
        // Start is called before the first frame update
        void Start()
        {
            rt = GetComponent<RectTransform>();
            s = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            rt.anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position);
            s.value = 5 - playerScript.dashCoolDown;
        }
    }

}
