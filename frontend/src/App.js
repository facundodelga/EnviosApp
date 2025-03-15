import './App.css';
import Home from './components/Home';
import LoginContainer from './components/LoginContainer';
import { useState } from 'react';

function App() {
  const [token, setToken] = useState('');
  
  return (
    
    <div className="App">
      {!token.length > 0 ? <LoginContainer setToken = {setToken}/> : <Home setToken={() => token}/>}
      {console.log(token)}
    </div>
  );
}

export default App;
