import React from 'react';
import PropTypes from 'prop-types';
import {Link} from 'react-router-dom';
import { connect } from 'react-redux';
import GuestLinks from './guestLinks';
import AuthLinks from './authLinks';

const Header = ({loading, isAuthenticated}) => {

    
    return (<header>
        <div className="container">
        <div className="brand">
            <h1><Link to="/">PropertyPro</Link></h1>
        </div>
        <nav>
    {!loading && isAuthenticated ? (<AuthLinks />) : (<GuestLinks />) }
        </nav>
        </div>
    </header>);
}


Header.propTypes = {
    isAuthenticated: PropTypes.bool.isRequired,
    loading: PropTypes.bool.isRequired,
}

const mapStateToProps = state => ({
    isAuthenticated: state.auth.isAuthenticated,
    loading: state.auth.loading
});

export default connect(mapStateToProps)(Header);
