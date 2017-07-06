/// <binding BeforeBuild='sass' />
var gulp = require("gulp"),
    fs = require("fs"),
    sass = require("gulp-sass");

gulp.task("sass", function () {
    return gulp.src('Style/tablsud.scss')
        .pipe(sass())
        .pipe(gulp.dest('Content/css'));
});