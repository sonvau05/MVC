$(function () {
    $("#btnAddStudent").click(function () {
        $("#studentId").val("");
        $("#studentName").val("");
        $("#studentGrade").val("");
        $(".markInput").val("");
        $("#modalTitle").text("Thêm học sinh");
        $("#studentModal").show();
    });

    $("#btnCancel").click(() => $("#studentModal").hide());

    $("#studentForm").submit(function (e) {
        e.preventDefault();
        let id = $("#studentId").val();
        let data = {
            fullName: $("#studentName").val(),
            grade: $("#studentGrade").val(),
            marks: []
        };
        $(".markInput").each(function () {
            let score = $(this).val();
            if (score !== "")
                data.marks.push({ subjectId: $(this).data("subject"), score: parseFloat(score) });
        });

        if (id) {
            $.ajax({
                url: "/api/students/" + id,
                type: "PUT",
                data: JSON.stringify(data),
                contentType: "application/json",
                success: () => location.reload()
            });
        } else {
            $.ajax({
                url: "/api/students",
                type: "POST",
                data: JSON.stringify(data),
                contentType: "application/json",
                success: () => location.reload()
            });
        }
    });

    $(".btnEditStudent").click(function () {
        let id = $(this).data("id");
        $.get("/api/students", function (res) {
            let s = res.find(x => x.id == id);
            if (!s) return;
            $("#studentId").val(s.id);
            $("#studentName").val(s.fullName);
            $("#studentGrade").val(s.grade);
            $(".markInput").each(function () {
                let subjId = $(this).data("subject");
                let mk = s.marks.find(m => m.subjectId == subjId);
                $(this).val(mk ? mk.score : "");
            });
            $("#modalTitle").text("Sửa học sinh");
            $("#studentModal").show();
        });
    });

    $(".btnDeleteStudent").click(function () {
        if (!confirm("Xóa học sinh này?")) return;
        let id = $(this).data("id");
        $.ajax({ url: "/api/students/" + id, type: "DELETE", success: () => location.reload() });
    });

    $("#btnAddSubject").click(function () {
        let name = $("#txtNewSubject").val();
        if (!name) return;
        $.ajax({
            url: "/api/subjects",
            type: "POST",
            data: JSON.stringify({ name: name }),
            contentType: "application/json",
            success: () => location.reload()
        });
    });

    $(".btnEditSubject").click(function () {
        let id = $(this).data("id");
        let name = prompt("Tên mới:");
        if (!name) return;
        $.ajax({
            url: "/api/subjects/" + id,
            type: "PUT",
            data: JSON.stringify({ name: name }),
            contentType: "application/json",
            success: () => location.reload()
        });
    });

    $(".btnDeleteSubject").click(function () {
        if (!confirm("Xóa môn học này?")) return;
        let id = $(this).data("id");
        $.ajax({ url: "/api/subjects/" + id, type: "DELETE", success: () => location.reload() });
    });
});
