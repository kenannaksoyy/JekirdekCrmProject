import { Route, Routes } from 'react-router-dom';
import './App.css';
import CustomerManagement from './mainComps/CustomerManagementPage/CustomerManagement';
import Login from './mainComps/LoginPage/Login';
import Header from './mainSubComps/Header';
import PrivateRoute from './tools/guards/PrivateRoute';
import NotFound from './mainComps/NotFound/NotFound';

function App() {
  return (
    <div className="appContainer">
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
