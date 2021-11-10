const confirmDeletionNew = async (id, message, onDelete) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteNew(id, onDelete);
    }
};

const deleteNew = async (id, onDelete) => {
    return fetch(`https://localhost:5001/news/${id}/delete`,
        {
            method: "post"
        }).then(() => onDelete());
};