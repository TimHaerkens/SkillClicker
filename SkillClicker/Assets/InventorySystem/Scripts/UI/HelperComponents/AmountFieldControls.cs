using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.InventorySystem.Scripts.UI.HelperComponents
{
    public class AmountFieldControls : MonoBehaviour
    {
        public int minAmount = 1;
        public int maxAmount = 999;

        [SerializeField]
        private Button _minusButton;

        [SerializeField]
        private Button _plusButton;

        [SerializeField]
        private Button _maxButton;

        [SerializeField]
        private Button _minButton;

        [SerializeField]
        private InputField _inputField;

        public int amount
        {
            get { return int.Parse(_inputField.text); }
        }

        protected virtual void Awake()
        {
            if (_minusButton != null)
                _minusButton.onClick.AddListener(MinusClicked);

            if (_plusButton != null)
                _plusButton.onClick.AddListener(PlusClicked);

            if (_maxButton != null)
                _maxButton.onClick.AddListener(MaxClicked);

            if (_minButton != null)
                _minButton.onClick.AddListener(MinClicked);
        }

        public void Set(int min, int max)
        {
            this.minAmount = min;
            this.maxAmount = max;

            ValidateAmount();
        }

        protected virtual void MinusClicked()
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _inputField.text = (amount - 10).ToString();
            else
                _inputField.text = (amount - 1).ToString();

            ValidateAmount();
        }

        protected virtual void PlusClicked()
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _inputField.text = (amount + 10).ToString();
            else
                _inputField.text = (amount + 1).ToString();

            ValidateAmount();
        }

        protected virtual void MaxClicked()
        {
            _inputField.text = maxAmount.ToString();
            ValidateAmount();
        }

        protected virtual void MinClicked()
        {
            _inputField.text = minAmount.ToString();
            ValidateAmount();
        }

        protected virtual void ValidateAmount()
        {
            _inputField.text = Mathf.RoundToInt(Mathf.Clamp(amount, minAmount, maxAmount)).ToString();
        }
    }
}
