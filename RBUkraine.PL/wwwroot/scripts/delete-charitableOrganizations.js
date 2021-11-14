const confirmDeletionCharitableOrganizations = async (id, message, onDelete) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteCharitableOrganizations(id, onDelete);
    }
};

const deleteCharitableOrganizations = async (id, onDelete) => {
    return fetch(`https://localhost:5001/organizations/${id}/delete`,
        {
            method: "post"
        }).then(() => onDelete());
};