const confirmDeletionAnimal = async (id, message, onDelete) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteAnimal(id, onDelete);
    }
};

const deleteAnimal = async (id, onDelete) => {
    return fetch(`https://localhost:5001/animals/${id}/delete`,
        {
            method: "post"
        }).then(() => onDelete());
};