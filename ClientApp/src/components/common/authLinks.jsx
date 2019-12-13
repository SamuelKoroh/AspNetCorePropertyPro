import React from 'react';
import {NavLink} from 'react-router-dom';

const AuthLinks = () => <ul>
<li><NavLink to="all-listing.html">Listing</NavLink></li>
<li><NavLink to="/dashboard">Dashboard</NavLink></li>
<li><NavLink to="add-property.html">New Ads</NavLink></li>
<li><NavLink to="favourite.html">My Favourite</NavLink></li>
<li><NavLink to="edit-profile.html">Update Profile</NavLink></li>
<li className="dropdown">
    <NavLink to="javascript:void(0)" className="dropbtn">Admin</NavLink>
    <div className="dropdown-content">
    <NavLink to="manage-dealtype.html">Ads Types/Deals</NavLink>
    <NavLink to="manage-ads.html">Manage Ads</NavLink>
    <NavLink to="manage-user.html">Manage Users</NavLink>
    <NavLink to="manage-report.html">Reported Issues</NavLink>
    </div>
</li>
<li id="signOut"><NavLink to="index.html">Sign Out</NavLink></li>
</ul>

export default AuthLinks
