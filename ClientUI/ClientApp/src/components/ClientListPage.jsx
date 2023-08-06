// ClientListPage.js
import React, { useState } from 'react';
import './ClientListPage.css'; // Import the CSS file for styling

const initialClients = [
    { id: 1, firstName: 'John', lastName: 'Doe',  email: 'john@example.com' },
    { id: 2, firstName: 'Jane', lastName: 'Doe',email: 'jane@example.com' },
  // Add more sample clients as needed
];

const ClientListPage = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [clients, setClients] = useState(initialClients);
    const [selectedClient, setSelectedClient] = useState(null);

    const openModal = (client) => {
        setSelectedClient(client);
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setSelectedClient(null);
        setIsModalOpen(false);
    };

    const handleFormSubmit = (e) => {
        e.preventDefault();
        const updatedClient = {
            id: selectedClient.id,
            firstName: e.target.firstName.value,
            lastName: e.target.lastName.value,
            email: e.target.email.value,
        };
        const updatedClients = clients.map(client =>
            client.id === updatedClient.id ? updatedClient : client
        );
        setClients(updatedClients);
        closeModal();
    };

    const handleSearch = (e) => {
        setSearchTerm(e.target.value);
    };

    const filteredClients = clients.filter(client =>
        client.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        client.lastName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="client-list-page">
            <h1>Client List</h1>
            <div className="search-bar">
                <img className="search-icon" src="search-icon.png" alt="Search Icon" />
                <input
                    type="text"
                    placeholder="Search by name..."
                    value={searchTerm}
                    onChange={handleSearch}
                />
            </div>
            <button className="add-client-button" onClick={() => openModal(null)}>Add Client</button>

            {filteredClients.length === 0 ? (
                <p className="no-records">No records found.</p>
            ) : (
                    <div className="client-list">
                        <div className="client-header">
                            <p>First Name</p>
                            <p>Last Name</p>
                            <p>Email</p>
                        </div>
                        {filteredClients.map(client => (
                            <div className="client-item" key={client.id} onClick={() => openModal(client)}>
                                <p className="client-name">{client.firstName} {client.lastName}</p>
                                <p className="client-email">{client.email}</p>
                            </div>
                        ))}
                    </div>
                )}

            {isModalOpen && (
                <div className="modal">
                    <div className="modal-content">
                        <span className="close-modal" onClick={closeModal}>&times;</span>
                        <h2>{selectedClient ? 'Edit Client' : 'Create New Client'}</h2>
                        <form onSubmit={handleFormSubmit}>
                            <label htmlFor="firstName">First Name:</label>
                            <input
                                type="text"
                                id="firstName"
                                name="firstName"
                                defaultValue={selectedClient ? selectedClient.firstName : ''}
                                required
                            />

                            <label htmlFor="lastName">Last Name:</label>
                            <input
                                type="text"
                                id="lastName"
                                name="lastName"
                                defaultValue={selectedClient ? selectedClient.lastName : ''}
                                required
                            />

                            <label htmlFor="email">Email:</label>
                            <input
                                type="email"
                                id="email"
                                name="email"
                                defaultValue={selectedClient ? selectedClient.email : ''}
                                required
                            />

                            <button type="submit">
                                {selectedClient ? 'Update Client' : 'Create Client'}
                            </button>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default ClientListPage;
