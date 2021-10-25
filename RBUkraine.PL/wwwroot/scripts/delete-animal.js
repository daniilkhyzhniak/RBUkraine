const confirmDeletionAnimal = async (id, message) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteAnimal(id);
        location.reload();
    }
};

const deleteAnimal = async (id) => {
    return fetch(`https://localhost:5001/animals/${id}/delete`,
        {
            method: "post"
        });
};