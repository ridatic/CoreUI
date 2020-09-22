import axios from 'axios';
import { Link, Redirect } from 'react-router-dom'
import CIcon from '@coreui/icons-react'
import React, { Component } from 'react'
import { CButton, CCard, CCardBody, CCardGroup, CCol, CContainer, CForm, CInput, CInputGroup,
         CInputGroupPrepend, CInputGroupText, CRow } from '@coreui/react'
         
const fValid = ({ fErrors, ...rest }) => {
  let valid = true;
  
  Object.values(fErrors).forEach(val => {
    val.length > 0 && (valid = false);
  });
  
  return valid;
};

export default class Login extends Component {
  constructor(props){
    super(props);

    this.state = {
      Token: null,
      Login: null,
      Password: null,
      fErrors: {
        Login: "",
        Password: "",
      }
    };
  }

  handleBlur = e => {
    e.preventDefault();

    const {name, value}=e.target;
    let fErrors = { ...this.state.fErrors };

    switch (name) {
      case "Login": fErrors.Login = value.length === 0 ? "Login required" : ""; break;
      case "Password": fErrors.Password = value.length === 0 ? "Password required" : ""; break;
      default: break;
    }
    
    this.setState({ fErrors, [name]: value });
  };
  
  handleSubmit = e => {
    e.preventDefault();

    if(!fValid(this.state)){
      alert("Veuillez saisir le login et le mot de passe.");
      return;
    }
    else{
      const data = { login: this.state.Login, password: this.state.Password };

      axios.post('http://localhost:5000/api/authentication/login', data)
         .then(resultat => {
           localStorage.setItem('login', data.login);
           localStorage.setItem('password', data.password);
           localStorage.setItem('token', resultat.data.token);

           this.props.history.push('/dashboard');
         })         
         .catch(err => {
           console.log(err);
         })
    }
  }

  render() {
    const { fErrors } = this.state;
    const Token = localStorage.getItem("token");

    if (Token !== null) {
      return (
        <Redirect to="/dashboard" />
      )
    }
    else{
      return (
        <div className="c-app c-default-layout flex-row align-items-center">
          <CContainer>
            <CRow className="justify-content-center">
              <CCol md="8">
                <CCardGroup>
                  <CCard className="p-4">
                    <CCardBody>
                      <CForm noValidate>
                        <h1>Login</h1>
                        <p className="text-muted">Sign In to your account</p>
                        { fErrors.Login.length > 0 && (<span class="error text-danger">{fErrors.Login}</span>) }
                        <CInputGroup className="mb-3">
                          <CInputGroupPrepend>
                            <CInputGroupText>
                              <CIcon name="cil-user" />
                            </CInputGroupText>
                          </CInputGroupPrepend>
                          <CInput type="text" name="Login" placeholder="Login" onBlur={ this.handleBlur } />
                        </CInputGroup>
                        { fErrors.Password.length > 0 && (<span class="error text-danger">{fErrors.Password}</span>) }
                        <CInputGroup className="mb-4">
                          <CInputGroupPrepend>
                            <CInputGroupText>
                              <CIcon name="cil-lock-locked" />
                            </CInputGroupText>
                          </CInputGroupPrepend>
                          <CInput type="password" name="Password" placeholder="Password" onBlur={ this.handleBlur } />
                        </CInputGroup>
                        <CRow>
                          <CCol xs="6">
                            <CButton color="primary" className="px-4" onClick={ this.handleSubmit }>Login</CButton>
                          </CCol>
                          <CCol xs="6" className="text-right">
                            <CButton color="link" className="px-0">Forgot password?</CButton>
                          </CCol>
                        </CRow>
                      </CForm>
                    </CCardBody>
                  </CCard>
                  <CCard className="text-white bg-primary py-5 d-md-down-none" style={{ width: '44%' }}>
                    <CCardBody className="text-center">
                      <div>
                        <h2>Sign up</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut
                 labore et dolore magna aliqua.</p>
                        <Link to="/register">
                          <CButton color="primary" className="mt-3" active tabIndex={-1}>Register Now!</CButton>
                        </Link>
                      </div>
                    </CCardBody>
                  </CCard>
                </CCardGroup>
              </CCol>
            </CRow>
          </CContainer>
        </div>
      ) 
    }
  }
}