
    window.$ = jQuery;
if (window.coursesData) {
    const coursesData = window.coursesData;

    // Sample data
    //const coursesData = {
    //    'CS101': {
    //        name: 'Introduction to Programming',
    //        description: 'Basic concepts of programming using Python',
    //        students: [
    //            { id: 1, name: 'Alice Johnson', email: 'alice@example.com', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=100' },
    //            { id: 2, name: 'Bob Smith', email: 'bob@example.com', avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=100' }
    //        ]
    //    },
    //    'CS201': {
    //        name: 'Data Structures',
    //        description: 'Advanced data structures and algorithms',
    //        students: [
    //            { id: 3, name: 'Carol White', email: 'carol@example.com', avatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=100' },
    //            { id: 4, name: 'David Brown', email: 'david@example.com', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100' }
    //        ]
    //    },
    //    'CS301': {
    //        name: 'Algorithms',
    //        description: 'Algorithm design and analysis',
    //        students: [
    //            { id: 5, name: 'Eve Wilson', email: 'eve@example.com', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=100' }
    //        ]
    //    },
    //    'CS401': {
    //        name: 'Software Engineering',
    //        description: 'Software development lifecycle and best practices',
    //        students: [
    //            { id: 6, name: 'Frank Davis', email: 'frank@example.com', avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=100' }
    //        ]
    //    }
//};


    // Teacher's current courses (moved outside of document ready to ensure global scope)
    let teacherCourses = [];

    $(document).ready(function () {
        // Populate course select dropdown
        const courseSelect = $('#courseSelect');
        
        Object.keys(coursesData).forEach(courseId => {
            const course = coursesData[courseId]; // Access the course object using courseId
            courseSelect.append(`<option value="${course.id}">${course.id} - ${course.courseName}</option>`);
        });

        // Add Course Handler
        $('#addCourse').on('click', function () {
            const courseId = $('#courseSelect').val();
            if (courseId && !teacherCourses.includes(Id)) {
                teacherCourses.push(courseId);
                updateCoursesList();

                // Optional: Add some visual feedback
                $('#courseSelect').val('');
                $('#addCourse').addClass('success');
                setTimeout(() => {
                    $('#addCourse').removeClass('success');
                }, 1000);
            } else if (teacherCourses.includes(Id)) {
                alert('This course is already added.');
            }
        });

        // Close Modal Handler
        $('.close').on('click', function () {
            $('#studentModal').hide();
        });

        // Click outside modal to close
        $(window).on('click', function (event) {
            if ($(event.target).is('#studentModal')) {
                $('#studentModal').hide();
            }
        });

        // Initial courses list update
        updateCoursesList();
    });

    function updateCoursesList() {
        const coursesList = $('#coursesList');
        coursesList.empty();

        if (teacherCourses.length === 0) {
            coursesList.append('<p>No courses added yet. Select a course to get started.</p>');
            return;
        }

        teacherCourses.forEach(Id => {
            const course = coursesData[Id];
            const courseElement = $(`
            <div class="course-card">
                <div class="course-info">
                    <h4>${Id} - ${course.CourseName}</h4>
                    
                </div>
                <div class="course-actions">
                    <button onclick="showStudents('${Id}')">View Students</button>
                    <button onclick="removeCourse('${Id}')">Remove Course</button>
                </div>
            </div>
        `);
            coursesList.append(courseElement);
        });
    }

    // Make functions globally available
    window.showStudents = function (Id) {
        const course = coursesData[Id];
        const studentsList = $('#studentsList');
        studentsList.empty();

        if (course.students.length === 0) {
            studentsList.append('<p>No students in this course.</p>');
        } else {
            course.students.forEach(student => {
                const studentElement = $(`
                <div class="student-card">
                    <img src="${student.avatar}" alt="${student.name}" class="student-avatar">
                    <div class="student-info">
                        <h4>${student.name}</h4>
                        <p>${student.email}</p>
                    </div>
                </div>
            `);
                studentsList.append(studentElement);
            });
        }

        $('#studentModal').show();
    };

    // Function to remove a course
    window.removeCourse = function (courseId) {
        const index = teacherCourses.indexOf(Id);
        if (index > -1) {
            teacherCourses.splice(index, 1);
            updateCoursesList();
        }
    };
} else {
    console.error("Courses data is not available.");
}
