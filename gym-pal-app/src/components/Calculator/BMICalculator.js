import { useState } from 'react';
import {calculateFormService} from '../../services/BodyCalculator';
import '../../styles/BMICalculator.css';

function Calculator() {
  const [weight, setWeight] = useState(0);
  const [height, setHeight] = useState(0);
  const [message, setMessage] = useState('');
  const [bmi, setBMI] = useState('');

  const calculateBMI = () => {
    const bmiValue = calculateFormService(weight, height);

      if (bmiValue < 17) {
        setMessage('You need to gain weight!');
        setBMI('Your BMI is ' + bmiValue.toFixed(2));
      } else if (bmiValue >= 17 && bmiValue < 25) {
        setMessage('Your form is great!');
        setBMI('Your BMI is ' + bmiValue.toFixed(2));
      } else if (bmiValue >= 25) {
        setMessage('You need to lose some weight!');
        setBMI('Your BMI is ' + bmiValue.toFixed(2));
      }
  };

  return (
    <div className="calc">
      <h1>BMI Calculator</h1>
      <span>Check your body type <br></br> Please fill the fields below</span>

      <div className="area-input">
       <label htmlFor="weight">Weight (in kgs)</label>
        <input
          value={weight}
          type="text"
          placeholder="Weight (in kgs)"
          onChange={(e) => setWeight(e.target.value)}
        />

        <label htmlFor="weight">Height (in cm)</label>
        <input
          value={height}
          type="text"
          placeholder="Height (in cm)"
          onChange={(e) => setHeight(e.target.value)}
        />
        <button onClick={calculateBMI}>
          Calculate
        </button>

      </div>
      <h2> {message} {bmi} </h2>
    </div>
  );
}

export default Calculator;
