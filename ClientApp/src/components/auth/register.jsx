import React, {Fragment, useState} from 'react';

const Register = () => {
  const [formData, setFormData] = useState({
    email:'',
    firstName:'',
    lastName: '',
    phoneNumber: '',
    address:'',
    password:'',
    password2: ''
  });
  
  const handleTextChange = ({target}) => setFormData({...formData, [target.name]: target.value });
  
  const {
    email,
    firstName,
    lastName,
    phoneNumber,
    address,
    password,
    password2
  } = formData;
  
  const handleFormSubmit = e => {
    e.preventDefault();

    if(password !== password2){
      console.log("The password did not match");
      return;
    }

    console.log(formData);
  }
  
  return (
    <Fragment>
      <main>
        <div className="container">
          <h1 className="large text-primary">
            Create Your Profile
          </h1>
          <p className="lead"><i className="fas fa-user"></i> Let's get some information to make your profile stand out</p>
          <small>* = required field</small>
          <form className="form mt-30" onSubmit={e=> handleFormSubmit(e)}>
            <div className="form-group">
              <input type="text" placeholder="Email" className="input-control" name="email" onChange={e => handleTextChange(e)} value={email} required />
            </div>
            <div className="form-group">
              <input type="text" placeholder="First Name" className="input-control" name="firstName" onChange={e => handleTextChange(e)} value={firstName} required />
            </div>
            <div className="form-group">
              <input type="text" placeholder="Last Name" className="input-control" name="lastName" onChange={e => handleTextChange(e)} value={lastName} required />
            </div>
            <div className="form-group">
              <input type="text" placeholder="Phone Number" className="input-control" name="phoneNumber" onChange={e => handleTextChange(e)} value={phoneNumber} required />
            </div>
            <div className="form-group">
              <textarea placeholder="Address" className="input-control" name="address" onChange={e => handleTextChange(e)} value={address}></textarea>
            </div>
            <div className="form-group">
              <input type="password" placeholder="Password" className="input-control" name="password" onChange={e => handleTextChange(e)} value={password} required />
            </div>
            <div className="form-group">
              <input type="password" placeholder="Confirm password" className="input-control" name="password2" onChange={e => handleTextChange(e)} value={password2}  required />
            </div>

            <input type="submit" id="btnSignUp" className="btn btn-primary btn-block" />
          </form>
        </div>
      </main>
      <footer>
        <p>All right reserved PropertyPro &copy; 2019</p>
      </footer>
    </Fragment>
  )
}

export default Register
