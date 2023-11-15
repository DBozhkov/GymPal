import React, { useState } from 'react';
import * as fetchDataService from '../../services/fetchDataService.js';
import '../../styles/CreateSupplement.css';

function CreateSupplement() {
    const [formData, setFormData] = useState({
        SupplementName: '',
        Price: 0, 
        ImageUrl: '',
      });
      
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
  
    fetchDataService.createSupplements(formData)
      .then((createdSupplement) => {
        console.log('Supplement created:', createdSupplement);
  
        // Clear the form fields
        setFormData({
            SupplementName: '',
            Price: 0, 
            ImageUrl: '',
        });
  
        setError(null);
      })
      .catch((error) => {
        console.error('Error creating supplement:', error);
        setError('Failed to create supplement. Please try again.');
      });
  };

  return (
    <div className="create-supplement-form">
      <h2 className="form-title">Create a New Supplement</h2>
      {error && <div className="error">{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="SupplementName">Name</label>
          <input
            type="text"
            name="SupplementName"
            value={formData.SupplementName}
            onChange={handleChange}
            required
            className="form-input"
          />
        </div>

        <div className="form-group">
          <label htmlFor="Price">Price</label>
          <input
            type="number"
            name="Price"
            value={formData.Price}
            onChange={handleChange}
            required
            className="form-input"
          />
        </div>

        <div className="form-group">
          <label htmlFor="ImageUrl">Image URL</label>
          <input
            type="text"
            name="ImageUrl"
            value={formData.ImageUrl}
            onChange={handleChange}
            required
            className="form-input"
          />
        </div>

        <button type="submit" className="submit-button">
          Create Supplement
        </button>
      </form>
    </div>
  );
}

export default CreateSupplement;
