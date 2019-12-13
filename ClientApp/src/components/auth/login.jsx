import React, {Fragment, useState} from 'react';
import {login} from '../../actions/auth';
import {connect} from 'react-redux';
import PropTypes from 'prop-types'

const Login = ({login}) => {

  const [formData, setFormData] = useState({
    email: '',
    password: ''
  });

  const handleSelectedChange = ({target}) => setFormData({...formData, [target.name]: target.value});

  const {email, password} = formData;

  const handleFormSubmit = e =>{
    e.preventDefault();

    login(formData);
  }
    return (
        <Fragment>
      
    <main>
      <div className="container">
        <h1 className="text-primary">Sign In</h1>
        <p className="lead"><i className="fas fa-user"></i> Sign into Your Account</p>
        <form className="form" onSubmit={e=>handleFormSubmit(e)}>
          <div className="form-group">
            <input type="email"  
            placeholder="Email Address" 
            className="input-control" 
            name="email" 
            ame="email"             
            value={email}
            onChange={e => handleSelectedChange(e)}
            required 
            />
          </div>
          <div className="form-group">
            <input type="password"
             placeholder="Password" 
            className="input-control" 
            name="password" 
            value={password}
            onChange={e => handleSelectedChange(e)}
            required
            />
          </div>
          <input type="submit" id="btnSubmit" className="btn btn-primary btn-block mr-10" value="Login" />
          <input type="submit" id="btnForget" className="btn btn-danger btn-block" value="Forget Password" />
        </form>
        <p className="my-1">Don't have an account? <a href="signup.html">Sign Up</a></p>
      </div>
    </main>
    <footer>
      <p>All right reserved PropertyPro &copy; 2019</p>
    </footer>
        </Fragment>
    )
}
Login.propTypes = {
  login: PropTypes.func.isRequired,
}
export default connect(null,{login})(Login);
