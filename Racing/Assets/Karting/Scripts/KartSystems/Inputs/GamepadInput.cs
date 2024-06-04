using UnityEngine;

namespace KartGame.KartSystems
{
    public class GamepadInput : BaseInput
    {
        public enum Player { Player1, Player2 }
        public Player player;

        private string turnInputName;
        private string accelerateButtonName;
        private string brakeButtonName;

        private void Start()
        {
            if (player == Player.Player1)
            {
                turnInputName = "Player1_Horizontal";
                accelerateButtonName = "Player1_Accelerate";
                brakeButtonName = "Player1_Brake";
            }
            else if (player == Player.Player2)
            {
                turnInputName = "Player2_Horizontal";
                accelerateButtonName = "Player2_Accelerate";
                brakeButtonName = "Player2_Brake";
            }
        }

        public override InputData GenerateInput()
        {
            float turn = Input.GetAxis(turnInputName);
            bool accelerate = Input.GetButton(accelerateButtonName);
            bool brake = Input.GetButton(brakeButtonName);

            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turn
            };
        }
    }
}
