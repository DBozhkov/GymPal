import HomeCarrousel from './HomeCarrousel'
import useAuth from '../../hooks/useAuth';

export default function Home(){
    const { auth } = useAuth();
    return (
        <>
        {auth.isLogged && <HomeCarrousel />}
        </>
    );
};