const supplement = ({ supplement, onAddToCart }) => (
    <div className="col-lg-3 col-md-6 col-sm-6 d-flex firstSupl">
      <div className="card w-100 my-2 shadow-2-strong secondSupl">
        <img
          src={supplement.imageUrl}
          alt={supplement.name}
          style={{ aspectRatio: '1 / 1' }}
          className="card-img-top"
        />
        <div className="card-body d-flex flex-column">
          <h5 className="card-title">{supplement.supplementName}</h5>
          <p className="card-text">${supplement.price.toFixed(2)}</p>
          <div className="card-footer d-flex align-items-end pt-3 px-0 pb-0 mt-auto">
            <a href="#!" className="btn btn-primary shadow-0 me-1" onClick={() => onAddToCart(supplement)}>
              Add to cart
            </a>
          </div>
        </div>
      </div>
    </div>
  );

  export default supplement;