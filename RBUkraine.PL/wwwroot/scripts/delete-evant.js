const confirmDeletionEvent = async (id, message, onDelete) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteEvent(id, onDelete);
    }
};

const deleteEvent = async (id, onDelete) => {
    return fetch(`https://localhost:5001/events/${id}/delete`,
        {
            method: "post"
        }).then(() => onDelete());
};