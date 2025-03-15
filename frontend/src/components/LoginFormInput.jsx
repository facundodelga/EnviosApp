import React from 'react';

const FormInput = ({ label, type, id, name, required }) => (
    <div className="form-group">
        <label htmlFor={id}>{label}</label>
        <input type={type} id={id} name={name} required={required} />
    </div>
);

export default FormInput;