import React from 'react';
import {NavLink} from 'react-router-dom';

const GuestLinks = () => <ul>
<li><NavLink to="all-listing.html">Listing</NavLink></li>
<li className="guest"><NavLink to="/register">Register</NavLink></li>
<li className="guest"><NavLink to="/login">Log in</NavLink></li>
</ul>

export default GuestLinks
