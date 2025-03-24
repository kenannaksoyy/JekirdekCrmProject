import { Route, Routes } from 'react-router-dom';
import './App.css';
import CustomerManagement from './mainComps/CustomerManagementPage/CustomerManagement';
import Login from './mainComps/LoginPage/Login';
import Header from './mainSubComps/Header';
import PrivateRoute from './tools/guards/PrivateRoute';
import NotFound from './mainComps/NotFound/NotFound';

const style = {
  display: "flex",
  flexDirection:"column",
  gap:"1rem"
};

function App() {
  return (
    <div style={style} className="appContainer">
      <Header/>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/CustomerManagement" element = {<PrivateRoute><CustomerManagement/></PrivateRoute>}/>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>

  );
}

export default App;
