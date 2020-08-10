async function sendVote(that, postId, isUpVote) {
    let csrfTokenElement = document.getElementsByName("__RequestVerificationToken")[0];

    let json = {
        postId: postId,
        isUpVote: isUpVote
    };

    try {

        const appUrl = "https://192.168.0.197:5001/api/votes";
        let response = await fetch(appUrl, {
            method: "post",
            headers: {
                'RequestVerificationToken': csrfTokenElement.value,
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            body: JSON.stringify(json)
        });

        let data = await response.json();

        if (response.ok) {

            let upVoteElement = document.getElementById("up-votes");
            let downVoteElement = document.getElementById("down-votes");
            let votesScoreElement = document.getElementById("votes-score");

            upVoteElement.textContent = data.upVotes;
            downVoteElement.textContent = data.downVotes;
            votesScoreElement.textContent = data.votesScore;

            let iElement = that.getElementsByTagName("i")[0];

            if (iElement.classList.contains("fas")) {
                iElement.classList.remove("fas");
                iElement.classList.add("far");
            } else {
                let voteElements = document
                    .querySelectorAll("#votes-display i.fas");

                for (let element of voteElements) {
                    element.classList.remove("fas");
                    element.classList.add("far");
                }

                iElement.classList.remove("far");
                iElement.classList.add("fas");
            }
        }

    } catch (err) {
        console.log(err);
    }
}