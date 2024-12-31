pipeline {
    agent any

    environment {
        PATH = "/usr/bin/dotnet:$PATH"
        PROJECT_NAME = "ProjetJenkins"
        DOCKER_IMAGE = "oumaimaazz/${PROJECT_NAME}" 
        DOCKER_CREDENTIALS = "dockerhub-credentials" // ID des credentials Docker Hub
        REPO_URL = "git@github.com:oumaimaazz23/ProjetJenkins.git"
    }

    stages {
        stage('Checkout') {
            steps {
                // Cloner le dépôt GitHub
                git branch: 'main', url: "${REPO_URL}"
            }
        }

        stage('Restore Dependencies') {
            steps {
                // Restauration des dépendances .NET
                sh "dotnet restore 'Echallene 2024.sln'"
            }
        }

        stage('Build') {
            steps {
                // Compilation du projet
                sh "dotnet build 'Echallene 2024.sln' --configuration Release"
            }
        }

        stage('Test') {
            steps {
                // Exécution des tests unitaires
                sh "dotnet test 'ProjetAtelier_test/ProjetAtelier_test.csproj' --logger trx"
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Construire l'image Docker
                    sh "docker build -t ${DOCKER_IMAGE}:latest ."
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                    // Push de l'image Docker sur Docker Hub
                    withCredentials([usernamePassword(credentialsId: DOCKER_CREDENTIALS, usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        sh '''
                        echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin
                        docker push ${DOCKER_IMAGE}:latest
                        '''
                    }
                }
            }
        }
    }

    post {
        always {
            // Nettoyage après le pipeline
            cleanWs()
        }
        success {
            echo "Pipeline terminé avec succès !"
        }
        failure {
            echo "Le pipeline a échoué. Vérifiez les logs."
        }
    }
}
