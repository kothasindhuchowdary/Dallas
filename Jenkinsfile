pipeline {
    agent any
    options {
        skipStagesAfterUnstable()
    }
    stages {
        stage('Build') {
            steps {
                bat 'make' // Use 'bat' for Windows shell commands
            }
        }
        stage('Test'){
            steps {
                bat 'make check' // Use 'bat' for Windows shell commands
            }
        }
        stage('Deploy') {
            steps {
                bat 'make publish' // Use 'bat' for Windows shell commands
            }
        }
    }
}

