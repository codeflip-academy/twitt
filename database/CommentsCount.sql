USE Twitt
GO

CREATE VIEW CommentsCount AS
SELECT
    c.PostID,
    count(*) AS CommentCount
FROM Comment c
GROUP BY c.PostID

GO