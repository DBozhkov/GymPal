import Carousel from 'react-bootstrap/Carousel';
import HomeCarrouselImage from './HomeCarrouselImage';
import '../../styles/HomeCarrousel.css';
import {Link} from 'react-router-dom';

function CarouselFade() {
  return (
    <Carousel fade>
      <Carousel.Item>
      <Link to='/loseweight'>
        <HomeCarrouselImage text="First slide" src={`${process.env.PUBLIC_URL}/HomePics/fat.jpg`}/>
        <Carousel.Caption>
          <h3>Lose Weight</h3>
          <p>Suppliments for losing fat</p>
        </Carousel.Caption>
        </Link>
      </Carousel.Item>
      <Carousel.Item>
      <Link to='/gainweight'>
        <HomeCarrouselImage text="Second slide" src={`${process.env.PUBLIC_URL}/HomePics/skinny.jpg`}/>
        <Carousel.Caption>
          <h3>Get Bigger</h3>
          <p>Suppliments for building muscle mass</p>
        </Carousel.Caption>
        </Link>
      </Carousel.Item>
      <Carousel.Item>
        <Link to='/stayfit'>
        <HomeCarrouselImage text="Third slide" src={`${process.env.PUBLIC_URL}/HomePics/athletic.jpg`}/>
        <Carousel.Caption>
          <h3>Stay in shape</h3>
          <p>
            Suppliments that make you keep training
          </p>
        </Carousel.Caption>
        </Link>
      </Carousel.Item>
    </Carousel>
  );
}

export default CarouselFade;