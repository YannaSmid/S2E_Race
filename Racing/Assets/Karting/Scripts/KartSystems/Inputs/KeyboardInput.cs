using UnityEngine;

namespace KartGame.KartSystems
{
    public class KeyboardInput : BaseInput
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
                turnInputName = "Horizontal_P1";
                accelerateButtonName = "Accelerate_P1";
                brakeButtonName = "Brake_P1";
            }
            else if (player == Player.Player2)
            {
                turnInputName = "Horizontal_P2";
                accelerateButtonName = "Accelerate_P2";
                brakeButtonName = "Brake_P2";
            }
        }

        public override InputData GenerateInput()
        {
            float turn = Input.GetAxis(turnInputName);
            bool accelerate = Input.GetButton(accelerateButtonName);
            bool brake = Input.GetButton(brakeButtonName);

            Debug.Log($"{player} Turn: {turn}, Accelerate: {accelerate}, Brake: {brake}");

            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turn
            };
        }
    }
}
