using Assets.Scripts.Interaction.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UserInputController : CharacterMover
    {
        public AbAttack leftClickAttack;
        public AbAttack rightClickAttack;
        public AbAttack middleClickAttack; 

        void FixedUpdate()
        {
            applyInput(Input.GetAxis("Horizontal"), Input.GetKeyDown(KeyCode.W));

            //left click ===========================================
            if (Input.GetMouseButton(0) || Input.GetKeyDown("e"))
            {
                leftClickAttack.prepAttack(); 
            }
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp("e"))
            {
                leftClickAttack.releaseAttack(); 
            }
            //right click ===========================================
            if (Input.GetMouseButton(1) || Input.GetKeyDown("r"))
            {
                rightClickAttack.prepAttack(); 
            }
            if(Input.GetMouseButtonUp(1) || Input.GetKeyUp("r"))
            {
                rightClickAttack.releaseAttack(); 
            }
            //middle click ===========================================
            if (Input.GetMouseButton(2) || Input.GetKeyDown("q") )
            {
                middleClickAttack.prepAttack(); 
            }
            if(Input.GetMouseButtonUp(2) || Input.GetKeyUp("q"))
            {
                middleClickAttack.releaseAttack(); 
            }

        }
    }
}
