import React from 'react';
import {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';
import * as fetchDataService from '../../../services/fetchDataService'
import Supplement from '../Supplement';
import '../../../styles/Supplements.css';


const GainWeightSupplements = ({ onAddToCart }) => {

  const [supplements, setSupplements] = useState([]);

  useEffect(() => {
    fetchDataService.getSupplements()
      .then((data) => {
        console.log(data);
        setSupplements(data);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <section>
      <div className="container my-5">
        <header className="mb-4">
          <h3>New products</h3>
        </header>

        <div className="supplementRow">
          {supplements.map((supplement) => (
            <Supplement key={supplement.id} supplement={supplement} onAddToCart={onAddToCart} />
          ))}
        </div>

        <Link to="/create-supplement">
           <button className="create-supplement-button">
               Add Supplement
           </button>
        </Link>
      </div>
    </section>
  );
};

export default GainWeightSupplements;
