import Navbar from "./Navbar";


const Home = ({token}) => {
    
    return (
        <div>
            <Navbar />
            <h1>Home {token}</h1>
        </div>
    );
}

export default Home;