SELECT s."Id", s."Birthday", s."EducationCost", s."GroupId", s."IsFullTimeEducation", s."IsStateFunded", s."Name", s."Phone", g."Id", g."Name", g."OnlyFullTimeEducation", v."Id", v."Date", v."StudentId", v."SubjectId"
FROM "Students" AS s
         LEFT JOIN "Groups" AS g ON s."GroupId" = g."Id"
         LEFT JOIN "Visits" AS v ON s."Id" = v."StudentId"
WHERE (@__searchTextBox_Text_0 = '') OR (strpos(s."Name", @__searchTextBox_Text_0) > 0)
ORDER BY s."Name", s."Id", g."Id"